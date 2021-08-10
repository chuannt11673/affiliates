import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { take, tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private userSubject = new BehaviorSubject<{
        name: string;
        preferred_username: string;
        sub: string;
        isAuthenticated: boolean;
    }>(null);
    user$ = this.userSubject.asObservable();

    constructor(
        private httpClient: HttpClient, 
        @Inject('BASE_URL') private baseUrl: string) {
    }

    private tokenEndpoint = this.baseUrl + 'connect/token';
    private userInfoEndpoint = this.baseUrl + 'connect/userinfo';

    getUser() {
        const access_token = sessionStorage.getItem('access_token');
        if (!access_token)
            return;

        this.httpClient.get(this.userInfoEndpoint)
        .pipe(take(1))
        .subscribe((res: any) => {
            this.userSubject.next({
                ...res,
                isAuthenticated: !!res.sub
            });
        });
    }

    signin(username: string, password: string) {
        const formData = new URLSearchParams();
        formData.set('client_id', 'spa');
        formData.set('grant_type', 'password');
        formData.set('scope', 'openid profile offline_access Affiliates.WebAppAPI');
        formData.set('username', username);
        formData.set('password', password);

        return this.httpClient.post(this.tokenEndpoint, formData.toString(), {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;'
            }
        })
        .pipe(
            tap(
                (res: any) => {
                    this.setToken(res);
                }
            )
        );
    }

    signout() {
        sessionStorage.clear();
        this.userSubject.next(null);
    }

    refreshToken() {
        const refreshToken = sessionStorage.getItem('refresh_token');
        if (!refreshToken)
            return;

        const formData = new URLSearchParams();
        formData.set('client_id', 'spa');
        formData.set('grant_type', 'refresh_token');
        formData.set('refresh_token', refreshToken);

        return this.httpClient.post(this.tokenEndpoint, formData.toString(), {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;'
            }
        })
        .pipe(
            tap(
                (res: any) => {
                    this.setToken(res);
                }
            )
        );
    }

    private setToken(res: any) {
        sessionStorage.setItem('access_token', res.access_token);
        sessionStorage.setItem('refresh_token', res.refresh_token);
        sessionStorage.setItem('expires_in', res.expires_in);
        this.getUser();
    }
}