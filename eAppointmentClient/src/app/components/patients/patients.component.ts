import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PatientModel } from '../../models/patient.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientPipe } from '../../pipe/patient.pipe';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [CommonModule,FormsModule,FormValidateDirective,PatientPipe,RouterLink],
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.css'
})
export class PatientsComponent implements OnInit {
  //Şuanki yaşamdöngüsüne dahil etmek için OnInit implementasyonu yaptık
  patients: PatientModel[] = [];


  

  @ViewChild('addModalCloseBtn') addModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;
  @ViewChild('updateModalCloseBtn') updateModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  updateModel: PatientModel = new PatientModel();
  createModel: PatientModel = new PatientModel();
  search: string="";

  constructor(
    private http: HttpService, 
    private swal: SwalService,
    public auth: AuthService
  ) {}

  ngOnInit(): void {
    this.getAll(); //doctor listem uygulamanın başında çağırılmalı
  }

  getAll() {
    this.http.post<PatientModel>('Patients/GetAll', {}, (res) => {
      this.patients = res.data;
    });
  }
  add(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Patients/Create', this.createModel, (res) => {
        this.swal.callToast(res.data, 'success');
        this.getAll();
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new PatientModel();
      });
    }
  }

  delete(id: string, fullname: string) {
    this.swal.callSwal(
      'Delete patient',
      `You want to delete ${fullname}?`,
      () => {
        this.http.post('Patients/DeleteById', { id: id }, (res) => {
          this.swal.callToast(res.data, 'info');
          this.getAll();
        });
      }
    );
  }

  get(data: PatientModel){
    this.updateModel={...data};
   
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Patients/Update', this.updateModel, (res) => {
        this.swal.callToast(res.data, 'success');
        this.getAll();
        this.updateModalCloseBtn?.nativeElement.click();
      });
    }
  }
}
