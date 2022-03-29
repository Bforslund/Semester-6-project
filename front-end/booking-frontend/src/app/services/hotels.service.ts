import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Hotel } from '../models/Hotel';
import { AvailabilitySearch } from '../models/AvailabilitySearch';
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
    return this.httpClient.get('http://localhost:43572/Hotel/hotels', this.httpOptions);
  }


  public getHotelById(id: number){
    return this.httpClient.get('http://localhost:43572/Hotel/' + id, this.httpOptions);
  }
  public checkAvailability(hotelId:number, dates: AvailabilitySearch){
 return this.httpClient.post('http://localhost:2161/Booking/availability/' + hotelId, dates, this.httpOptions);
  }

  


}
