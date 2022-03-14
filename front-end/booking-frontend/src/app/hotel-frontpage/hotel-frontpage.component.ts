import { Component, OnInit } from '@angular/core';
import {Hotel} from '../models/Hotel';
import { Router } from '@angular/router';

const ELEMENT_DATA: Hotel[] = [
  {id: 1, title: 'Amsterdam hotel', info: 'tbd'},
  {id: 2, title: 'Stockholm hotel', info: 'tbd'},
  {id: 3, title: 'Den haag hotel', info: 'tbd'},
];


@Component({
  selector: 'app-hotel-frontpage',
  templateUrl: './hotel-frontpage.component.html',
  styleUrls: ['./hotel-frontpage.component.css']
})
export class HotelFrontpageComponent implements OnInit {
  constructor(private router: Router){}
  hotelList: Hotel[] | undefined;
  ngOnInit(): void {
    this.hotelList = ELEMENT_DATA;

  }
  openHotelDetails(hotel: Hotel): void {
    this.router.navigate(['/details/' + hotel.id]);
    
   
   }
}
