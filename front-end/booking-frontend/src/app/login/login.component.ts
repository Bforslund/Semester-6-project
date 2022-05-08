import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticateRequest } from '../models/AuthenticateRequest';
import { AdminService } from '../services/admin.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  token:string = "";
  loggedIn:boolean = false;
  isLoginError : boolean = false;
  request = new AuthenticateRequest("", "");
  constructor(private router : Router, private service: AdminService) { }

  ngOnInit(): void {
    if(this.readLocalStorageValue() != null){
      this.loggedIn= true;
      this.router.navigate(['/adminpage']);
    }else{
      this.loggedIn = false;

  }
}
readLocalStorageValue() {
  return localStorage.getItem('userToken');
}
OnSubmit(){
this.service.login(this.request)
.subscribe(
  (res: any) => {
   localStorage.setItem('userToken', res.token);
   location.reload();
   this.router.navigate(['/adminpage']);
  },
  (error: Response) => {
    if(error.status === 404){
      console.log("not found");
      this.isLoginError = true;
     }
    }
);
}



}
