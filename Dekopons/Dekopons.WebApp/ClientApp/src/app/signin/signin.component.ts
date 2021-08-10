import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_core/services/auth.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent implements OnInit {

  form: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const username = this.activatedRoute.snapshot.queryParamMap.get('username');
    this.form = this.formBuilder.group({
      username: this.formBuilder.control(username || '', Validators.required),
      password: this.formBuilder.control('', Validators.required)
    });
  }

  signin() {
    if (!this.form.valid)
      return;

    const value = this.form.value;
    this.authService.signin(value.username, value.password).subscribe(_ => {
      this.router.navigate(['']);
    });
  }

}
