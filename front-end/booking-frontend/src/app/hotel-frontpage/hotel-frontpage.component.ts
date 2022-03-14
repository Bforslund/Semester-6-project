import { Component, OnInit } from '@angular/core';
import {Hotel} from '../models/Hotel';
import { Router } from '@angular/router';
import { HotelsService } from '../services/hotels.service';
import { Observable } from 'rxjs';




@Component({
  selector: 'app-hotel-frontpage',
  templateUrl: './hotel-frontpage.component.html',
  styleUrls: ['./hotel-frontpage.component.css']
})
export class HotelFrontpageComponent implements OnInit {
  constructor(private router: Router, private hotelService: HotelsService){}
  hotelList: Hotel[] = [];
  ngOnInit(): void {
    this.hotelService.getAllHotels()
    .subscribe((data)=>{
     this.hotelList = <Hotel[]>data;
  });


  }
  openHotelDetails(hotel: Hotel): void {
    this.router.navigate(['/details/' + hotel.id]);
    
   
   }
}
