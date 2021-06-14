import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private authService: AuthService) { }
  user: any;

  ngOnInit(): void {
    this.authService.user$.subscribe(res => {
      this.user = res;
    });
  }

  signout() {
    this.authService.signout();
  }

}
