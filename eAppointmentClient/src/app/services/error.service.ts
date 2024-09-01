import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private swal: SwalService) { }
  errorHandler(err: HttpErrorResponse){
    console.log(err);
    let message ="Error!"
    if(err.status===0){
      this.swal.callToast("API is not available","error");
    }else if(err.status===401){
      this.swal.callToast("You are not authorized");
    }
    else if(err.status===404){
      this.swal.callToast("API not found");
    }else if(err.status===500){
      message="";
        for(const e of err.error.errorMessages){
          message +=e+"\n";
        }     
      }
    


    this.swal.callToast(message,"error")
  }
}
