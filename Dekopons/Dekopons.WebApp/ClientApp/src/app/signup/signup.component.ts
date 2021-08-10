import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityClient } from '../api';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  form: FormGroup;
  constructor(private formBuilder: FormBuilder, private identityClient: IdentityClient, private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: this.formBuilder.control(null, Validators.required),
      password: this.formBuilder.control(null, Validators.required),
      email: this.formBuilder.control(null, Validators.required)
    });
  }

  signup() {
    if (!this.form.valid)
      return;

    const value = this.form.value;
    this.identityClient.create(value).subscribe(res => {
      this.router.navigate(['signin'], { queryParams: { username: res.username } });
    });
  }

}
