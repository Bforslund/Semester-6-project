import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Hotel } from '../models/Hotel';
import { AvailabilitySearch } from '../models/AvailabilitySearch';
import { Booking } from '../models/Booking';
import { Room } from '../models/Room';
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
    return this.httpClient.get('http://hotel.bea.local:9080/Hotel/hotels', this.httpOptions);
  }
  public getAllReservedRooms(){
    return this.httpClient.get('http://hotel.bea.local:9080/Hotel/getAllReservedRooms', this.httpOptions);
  }

  public getHotelById(id: number){
    return this.httpClient.get('http://hotel.bea.local:9080/Hotel/' + id, this.httpOptions);
  }
  public checkAvailability(hotelId:number, dates: AvailabilitySearch){
 return this.httpClient.post('http://booking.bea.local:9080/Booking/availability/' + hotelId, dates, this.httpOptions);
  }
  public createBooking(booking:Booking){
    return this.httpClient.post('http://booking.bea.local:9080/Booking/', booking, this.httpOptions);
  }

  public addNewHotel(hotel:Hotel){
    return this.httpClient.post('http://hotel.bea.local:9080/Hotel/newHotel/', hotel, this.httpOptions);
     }

  public getAllRoomsOfHotel(hotelId:number){
    return this.httpClient.get('http://hotel.bea.local:9080/Hotel/rooms/' + hotelId, this.httpOptions);
  }
  
  public addNewRoom(hotelId:number, room:Room){
    return this.httpClient.post('http://hotel.bea.local:9080/Hotel/addRooms/' + hotelId, room, this.httpOptions);
     }
  public getAvailableRooms(hotelId:number, dates: AvailabilitySearch){
      return this.httpClient.post('http://booking.bea.local:9080/Booking/availableRooms/' + hotelId,dates, this.httpOptions);
    }
   

}
