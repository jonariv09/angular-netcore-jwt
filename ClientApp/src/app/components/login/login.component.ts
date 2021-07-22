import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {UserService} from "../../services/user.service";
import {Authenticate} from "../../Authenticate/Authenticate";
import {AuthenticationService} from "../../_helpers/authentication.service";
import {ActivatedRoute, Router} from "@angular/router";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  returnUrl: string;

  userForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {

    if(this.authenticationService.currentUserValue) {
      router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get formControls() { return this.userForm.controls; }

  onSubmit() {
    if(this.userForm.dirty) {

      this.authenticationService.login(this.formControls.username.value, this.formControls.password.value)
        .pipe(first())
        .subscribe(data => {
            this.router.navigate([this.returnUrl]);
        });
    }
  }

}
