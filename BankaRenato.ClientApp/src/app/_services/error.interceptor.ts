import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(error => {
            if (error instanceof HttpErrorResponse) {
                // Unauthorized 401
                if (error.status === 401){
                    return throwError(error.error);
                }
                // Internal error 500
                const applicationError = error.headers.get('Application-Error');
                if (applicationError) {
                    return throwError(applicationError);
                }
                // Bad request 400
                const serverError = error.error;
                let modelStateErrors = '';
                if (serverError && typeof serverError === 'object') {
                    for (const key in serverError) {
                        modelStateErrors += serverError[key] + '\n';
                    }
                }
                return throwError(modelStateErrors || serverError || 'Server error');
                
            }
        }));
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
}