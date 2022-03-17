import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {FormGroup, FormControl, NgForm, Validators, FormGroupDirective} from '@angular/forms';
import { HotelsService } from '../services/hotels.service';
import { Hotel } from '../models/Hotel';
import {ErrorStateMatcher} from '@angular/material/core';


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
  hotel = new Hotel(1,"","",1);
  constructor(private route: ActivatedRoute, private service: HotelsService) { }


  emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  matcher = new MyErrorStateMatcher();


  ngOnInit(): void {
 this.id = Number(this.route.snapshot.paramMap.get('id'));
 this.service.getHotelById(this.id)
    .subscribe((data)=>{
      console.log(data);
     this.hotel = <Hotel>data;
    });

}
range = new FormGroup({
  start: new FormControl(),
  end: new FormControl(),
});
}
