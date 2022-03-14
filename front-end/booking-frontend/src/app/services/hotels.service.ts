import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class HotelsService {

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  public getAllHotels(){
    return this.httpClient.get('http://localhost:49162/Hotel/hotels', this.httpOptions);
  }


}
