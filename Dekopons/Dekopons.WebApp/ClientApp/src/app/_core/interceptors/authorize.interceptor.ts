import { Injectable } from '@angular/core';
import {
    HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders
} from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class AuthorizeInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const token = sessionStorage.getItem('access_token');

        if (token) {
            req = req.clone({
                headers: new HttpHeaders({
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': req.headers.get('Content-Type') ?? 'application/json'
                })
            })
        }
        
        return next.handle(req);
    }
}