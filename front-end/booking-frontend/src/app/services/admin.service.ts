import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { AuthenticateRequest } from '../models/AuthenticateRequest';
import jwt_decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class AdminService {
  readLocalStorageValue() {
    if(localStorage.getItem("userToken") != null){
      this.httpOptions.headers = this.httpOptions.headers.set('Authorization',  localStorage.getItem("userToken")!);
    };
}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) {
    this.readLocalStorageValue();
   }


  login(request: AuthenticateRequest){
    return this.httpClient.post('http://localhost:43572/Admin/login', request, this.httpOptions);
   }

  logout(){
    this.httpOptions.headers = this.httpOptions.headers.delete('Authorization');
  }
  getDecodedAccessToken(token: string): any {
    try{
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }

  getHotelIdOfLoggedIn(){
    var decoded = this.getDecodedAccessToken(localStorage.getItem("userToken")!)
    console.log(JSON.stringify(decoded));
    var id = decoded['id'];
    return id;
  }

 
}
