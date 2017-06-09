﻿import {Injectable} from "@angular/core";
import {Http, Response} from "@angular/http";
import {Observable} from "rxjs/Observable";

@Injectable()
export class AppService {
    constructor(private http: Http) { }

     // URL to web api
    private loungeBaseUrl = 'api/lounge/';
    private placeBaseUrl = 'api/place/'; 

    getLatestDiscussion(num?: number) {
        var url = this.loungeBaseUrl + "GetLatestDiscussion/";
        if (num != null) url += num;
        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }    

    getLatestEntries(num?: number) {
        var url = this.placeBaseUrl + "GetLatestEntries/";
        if (num != null) url += num;
        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }

    getMostViewed(num?: number) {
        var url = this.placeBaseUrl + "GetMostViewed/";
        if (num != null) url += num;
        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }   

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    }
}
