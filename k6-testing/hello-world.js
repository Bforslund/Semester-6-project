import http from 'k6/http';

export const options = {
    vus: 250,
    duration: '30s',
  };

export default function(){
    http.get('https://booking.d28c7b7324414a4cba3b.germanywestcentral.aksapp.io/Booking/hello');
}