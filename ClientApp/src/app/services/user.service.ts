import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { User } from '../Entities/User';
import {Authenticate} from "../Authenticate/Authenticate";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  url: string
  controller: string;

  constructor(private http: HttpClient) {
    this.url = 'https://localhost:44338/users';
  }

  getUsers() {
    let token = this.getToken();
    return this.http.get<User[]>(this.url + '/getAll').toPromise();
  }

  getToken() {
    return localStorage.getItem('token');
  }

}
