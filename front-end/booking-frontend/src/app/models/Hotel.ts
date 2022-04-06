import { Room } from "./Room";

export class Hotel{
    public id: number =  0;
    public rooms: Room[];
    public roomsByType: any; 
    constructor(
        public title: string,
        public info: string,
        
    ) { 
       this.rooms = [];
     }

}