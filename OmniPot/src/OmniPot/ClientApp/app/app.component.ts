import { Component } from "@angular/core";
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { Http } from '@angular/http';
import { AuthService } from './security/auth.service';

@Component({
    selector: "app-root",
    templateUrl : './template/app.component.html' 
})


export class AppComponent {
    title = "The Backpackers' Lounge";
    subTitle = "For geeks who want to explore nature beyond limits.";

    public constructor(private router: Router, private titleService: Title, private http: Http, private authService: AuthService) { }

    // wrapper to the Angular title service.
    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }

    // provide local page the user's logged in status (do we have a token or not)
    public isLoggedIn(): boolean {
        var val = this.authService.loggedIn();
        alert(val);
        return val;
    }

    // tell the server that the user wants to logout; clears token from server, then calls auth.service to clear token locally in browser
    //public logout() {
    //    this.http.get('/connect/logout', { headers: this.authService.authJsonHeaders() })
    //        .subscribe(response => {
    //            // clear token in browser
    //            this.authService.logout();
    //            // return to 'home' page
    //            this.router.navigate(['']);
    //        },
    //        error => {
    //            // failed; TODO: add some nice toast / error handling
    //            alert(error.text());
    //            console.log(error.text());
    //        }
    //        );
    //}

}