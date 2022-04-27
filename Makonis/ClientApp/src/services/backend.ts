import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { Person } from '../model/person';

@Injectable({
    providedIn: 'root'
})
export class BackendService {
    baseUrl;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }
    insertPersonDetails(person: Person): Observable<Person> {

        const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json', }), responseType: 'text' as 'json' };
        return this.http.post<Person>(this.baseUrl + 'person', person, httpOptions);

    }

}