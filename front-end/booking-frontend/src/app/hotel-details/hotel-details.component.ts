import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {FormGroup, FormControl, NgForm, Validators, FormGroupDirective} from '@angular/forms';
import { HotelsService } from '../services/hotels.service';
import { Hotel } from '../models/Hotel';
import {ErrorStateMatcher} from '@angular/material/core';
import { AvailabilitySearch } from '../models/AvailabilitySearch';
import { Booking } from '../models/Booking';
import { Room } from '../models/Room';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css']
})
export class HotelDetailsComponent implements OnInit {
  id: number | undefined;
  hotel = new Hotel(1,"","",1, "");
  room = new Room(3, "Economy");
  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });
  startD: any;
  endD:any;
  available:number = 0;
  constructor(private route: ActivatedRoute, private service: HotelsService) { }


  emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  matcher = new MyErrorStateMatcher();


  ngOnInit(): void {
 this.id = Number(this.route.snapshot.paramMap.get('id'));
 
 this.service.getHotelById(this.id)
    .subscribe((data)=>{
     this.hotel = <Hotel>data;
     console.log(this.hotel)
    });

}



submit(data:any, hotel:Hotel) {
  var dates = new AvailabilitySearch(data.start,  data.end);
  this.service.checkAvailability(hotel.id, dates).subscribe((res: any)=>{
    this.available = <number>res;
  },       (error: Response) => {
      console.log(error);
  });

}

MakeBooking(data:any) {
console.log("New booking with " + data);
  var booking = new Booking(2, this.hotel.id, this.room, data, this.startD, this.endD);
  console.log(booking);
  this.service.createBooking(booking).subscribe((res: any)=>{
    console.log("booking made" + booking);
  },       (error: Response) => {
      console.log(error);
  });

}



}
