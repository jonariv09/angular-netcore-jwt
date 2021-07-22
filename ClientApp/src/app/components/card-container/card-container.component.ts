import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../Entities/User";

@Component({
  selector: 'app-card-container',
  templateUrl: './card-container.component.html',
  styleUrls: ['./card-container.component.css']
})
export class CardContainerComponent implements OnInit {
  users: User[];

  constructor(private userService: UserService) { }

  async ngOnInit() {
    await this.loadUsers();
  }

  async loadUsers() {
    this.users = await this.userService.getUsers();
    console.log(this.users);
  }


}
