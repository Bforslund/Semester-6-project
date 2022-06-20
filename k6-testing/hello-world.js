import { sleep } from 'k6';
import http from 'k6/http';

export const options = {
    vus: 2000,
    duration: '30s',
  };

export default function(){
    http.get('https://hotelquery.d28c7b7324414a4cba3b.germanywestcentral.aksapp.io/Hotel/1');
    sleep(10);
}