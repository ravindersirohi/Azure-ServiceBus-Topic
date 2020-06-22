import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  message: string = '';

  serviceMessage = <ServiceMessage>{};

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  sendmessage(e) {
    console.info('Message sent');
    this.serviceMessage.name = 'Test User'
    this.serviceMessage.message = this.message;
    console.log(this.message);
    this.http.post<ServiceMessage>(this.baseUrl + 'message', this.serviceMessage, this.httpOptions).subscribe(result => {
      console.log(result);
      this.message = '';
    },
      error => console.error(error)
    );
  }
  readmessage(e) {
    console.info('Read Message');
    this.http.get<ServiceMessage[]>(this.baseUrl + 'message').subscribe(result => {
      console.log(result);
    },
      error => console.error(error)
    );
  }
}

interface ServiceMessage {
  name: string;
  message: string;
}
