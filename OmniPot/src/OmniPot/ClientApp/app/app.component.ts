import { Component, OnInit} from "@angular/core";
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { Http } from '@angular/http';
import { AuthService } from './security/auth.service';

@Component({
    selector: "app-root",
    templateUrl : './template/app.component.html' 
})


export class AppComponent implements OnInit{
    title = "Agrisoft";

    public constructor(private router: Router, private titleService: Title, private http: Http, private authService: AuthService ) { }

    ngOnInit() {        

        //setting title
        this.setTitle('Agrisoft');

        // subscribe to form changes  
        this.isLoggedIn();
    }

    // wrapper to the Angular title service.
    setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }

    // provide local page the user's logged in status (do we have a token or not)
    isLoggedIn() {
        var val = this.authService.loggedIn();        
        if (!this.authService.loggedIn()) {
            this.router.navigate(['/login']);
        }
    }
}