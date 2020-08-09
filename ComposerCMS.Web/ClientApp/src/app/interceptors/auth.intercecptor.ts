import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authenticationToken = JSON.parse(localStorage.getItem("userToken"));

    console.log(authenticationToken);

    if (authenticationToken) {
      const cloned = req.clone({
        headers: req.headers.set("Authorization", "Bearer " + authenticationToken.token)
      });

      return next.handle(cloned);
    }
    else {
      return next.handle(req);
    }
  }
}
