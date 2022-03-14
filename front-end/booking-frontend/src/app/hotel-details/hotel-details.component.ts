import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {FormGroup, FormControl} from '@angular/forms';
@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css']
})
export class HotelDetailsComponent implements OnInit {
  id: number | undefined;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
 this.id = Number(this.route.snapshot.paramMap.get('id'));
 
}
range = new FormGroup({
  start: new FormControl(),
  end: new FormControl(),
});
}
