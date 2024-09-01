import { Component, ElementRef, ViewChild } from '@angular/core';
import { departments } from '../../constants';
import { DoctorModel } from '../../models/doctor.model';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule, DatePipe, NgFor } from '@angular/common';
import {DxSchedulerModule, DxServerTransferStateModule} from 'devextreme-angular';
import { HttpService } from '../../services/http.service';
import { AppointmentModel } from '../../models/appointment.model';
import { CreateAppointmentModel } from '../../models/create-appointment.model';
import { createApplication } from '@angular/platform-browser';
import { FormValidateDirective } from 'form-validate-angular';
import { identity } from 'rxjs';
import { PatientModel } from '../../models/patient.model';
import { SwalService } from '../../services/swal.service';
import { AuthService } from '../../services/auth.service';

declare const $:any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule,CommonModule,DxSchedulerModule,FormValidateDirective],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers:[DatePipe]
})
export class HomeComponent {
  departments=departments;
  doctors: DoctorModel[]=[];

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> |undefined;

  selectedDepartmentValue:number=0;
  selectedDoctorId: string="";

  appointments:AppointmentModel[]=[]
  createModel: CreateAppointmentModel = new CreateAppointmentModel();

  constructor(
    private http:HttpService,
    private date:DatePipe,
    private swal: SwalService,
    private auth: AuthService
  ){

  }

  getAllDoctor(){
    this.selectedDoctorId ="";
    if(this.selectedDepartmentValue>0){
      this.http.post<DoctorModel[]>("Appointments/GetAllDoctorByDepartment", {departmentValue: +this.selectedDepartmentValue}, (res)=>{    //+işareti stringi değer tipe çevirdi (int)
        this.doctors=res.data;
      })
    }
  }

  GetAllAppointments(){
    if(this.selectedDoctorId){
      this.http.post<AppointmentModel[]>("Appointments/GetAllByDoctorId", 
        {doctorId: this.selectedDoctorId}, (res)=>{    //+işareti stringi değer tipe çevirdi (int)
        this.appointments=res.data;
      })
    }
  }

  onAppointmentFormOpening(e:any){
    e.cancel=true;
    if (this.auth.tokenDecode.roles.includes("Admin")){
      
      this.createModel.startDate= this.date.transform(e.appointmentData.startDate, "dd.MM.yyyy HH:mm")??"";
      this.createModel.endDate= this.date.transform(e.appointmentData.endDate, "dd.MM.yyyy HH:mm")??"";
      this.createModel.doctorId=this.selectedDoctorId;
      $("#addModal").modal("show")
    }
    
  }

  

  getPatient(){
    this.http.post<PatientModel>("Appointments/GetPatientByIdentityNumber",{identityNumber: this.createModel.identityNumber},res=>{
      if(res.data===null){
        this.createModel.firstName="";
        this.createModel.lastName="";
        this.createModel.city="";
        this.createModel.town="";
        this.createModel.fullAddress="";
        this.createModel.patientId=null;
        return;
      }
      
      this.createModel.patientId= res.data.id;
      this.createModel.firstName= res.data.firstName;
      this.createModel.lastName= res.data.lastName;
      this.createModel.city= res.data.city;
      this.createModel.town= res.data.town;
      this.createModel.fullAddress= res.data.fullAddress;
    })

    
  }

  create(form:NgForm){
    if(form.valid){
      this.http.post<string>("Appointments/Create", this.createModel,res=>{
        this.swal.callToast(res.data);
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel=new CreateAppointmentModel();
        this.GetAllAppointments();
      })
    }
  }

  onAppointmentDeleted(e:any){
    e.cancel=true;
    
  }

  onAppointmentDeleting(e:any){
    e.cancel=true;
    if(this.auth.tokenDecode.roles.includes("Admin")){
      this.swal.callSwal("Delete appointment ?",`You want to delete ${e.appointmentData.patient.fullName} appointment ?`,()=>{
        this.http.post<string>("Appointments/DeleteById",{id: e.appointmentData.id},res=>{
          this.swal.callToast(res.data,"info")
          this.GetAllAppointments();
        })
      })
    }
    
  }

  onAppointmentUpdating(e:any){
      e.cancel=true;
      if(this.auth.tokenDecode.roles.includes("Admin")) {
        const data= {
          id: e.oldData.id,
          startDate: this.date.transform(e.newData.startDate,"dd.MM.yyyy HH:mm"),
          endDate: this.date.transform(e.newData.endDate,"dd.MM.yyyy HH:mm"),
        };
  
        this.http.post("Appointments/Update",data,res=>{
          this.swal.callToast(res.data)
          this.GetAllAppointments();
        })
      } 
   
  }
}
