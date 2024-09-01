import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  viewChild,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { DoctorModel } from '../../models/doctor.model';
import { HttpService } from '../../services/http.service';
import { CommonModule, NgFor } from '@angular/common';
import { departments } from '../../constants';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { SwalService } from '../../services/swal.service';
import { DoctorPipe } from '../../pipe/doctor.pipe';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, FormValidateDirective,DoctorPipe], //commonmodule döngüler için, doctorları tabloya yazarken loop kullanıyoruz.
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.css',
})
export class DoctorsComponent implements OnInit {
  //Şuanki yaşamdöngüsüne dahil etmek için OnInit implementasyonu yaptık
  doctors: DoctorModel[] = [];
  departments = departments;

  

  @ViewChild('addModalCloseBtn') addModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;
  @ViewChild('updateModalCloseBtn') updateModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  updateModel: DoctorModel = new DoctorModel();
  createModel: DoctorModel = new DoctorModel();
  search: string="";

  constructor(
    private http: HttpService,
     private swal: SwalService,
     public auth: AuthService) {}
  ngOnInit(): void {
    this.getAll(); //doctor listem uygulamanın başında çağırılmalı
    
  }

  getAll() {
    this.http.post<DoctorModel>('Doctors/GetAll', {}, (res) => {
      this.doctors = res.data;
    });
  }
  add(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Doctors/Create', this.createModel, (res) => {
        this.swal.callToast(res.data, 'success');
        this.getAll();
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new DoctorModel();
      });
    }
  }

  delete(id: string, fullname: string) {
    this.swal.callSwal(
      'Delete doctor',
      `You want to delete ${fullname}?`,
      () => {
        this.http.post('Doctors/DeleteById', { id: id }, (res) => {
          this.swal.callToast(res.data, 'info');
          this.getAll();
        });
      }
    );
  }

  get(data: DoctorModel){
    this.updateModel={...data};
    this.updateModel.departmentValue = data.department.value;
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Doctors/Update', this.updateModel, (res) => {
        this.swal.callToast(res.data, 'success');
        this.getAll();
        this.updateModalCloseBtn?.nativeElement.click();
      });
    }
  }
}
