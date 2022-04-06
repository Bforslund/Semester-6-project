import { Hotel } from "./Hotel";
import { Room } from "./Room";

export class Booking{
    public Id:number = 1;
    constructor(
        public HotelId: number,
        public RoomId: number,
        public ContactInfo: string,
        public Start:string,
        public End:string 
    ) { 
       
     }

}