import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private userSubject = new BehaviorSubject<{ isLoggedIn: boolean, username?: string }>({
        isLoggedIn: false,
        username: null
    });
    user$ = this.userSubject.asObservable();

    constructor(private httpClient: HttpClient, 
        @Inject('BASE_URL') private baseUrl: string) {
        this.init();
    }

    init() {
        const access_token = localStorage.getItem('access_token');
        if (access_token) {
            this.userSubject.next({
                isLoggedIn: true
            });
        }
    }

    signin(username: string, password: string) {
        const tokenEndpoint = this.baseUrl + 'connect/token';
        const formData = new URLSearchParams();
        formData.set('client_id', 'spa');
        formData.set('grant_type', 'password');
        formData.set('scope', 'openid profile Affiliates.WebAppAPI');
        formData.set('username', username);
        formData.set('password', password);

        return this.httpClient.post(tokenEndpoint, formData.toString(), {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;'
            }
        })
        .pipe(
            tap(
                (res: any) => {
                    localStorage.setItem('access_token', res.access_token);
                    this.userSubject.next({
                        isLoggedIn: true,
                        username: username
                    });
                }
            )
        );
    }

    signout() {
        localStorage.clear();
        this.userSubject.next({
            isLoggedIn: false,
            username: null
        });
    }
}