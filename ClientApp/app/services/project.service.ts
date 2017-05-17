import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Project } from '../models/project';
import { Info } from '../models/info';

@Injectable() export class ProjectService {
    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {
        this.headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: this.headers });
    }

    public GetInfo(infoKey: string): Observable<Info> {
        return this.http.get(`/api/projects/info/${infoKey}`, this.options)
            .map(response => response.json() as Info)
            .catch(error => Observable.throw(error));
    }

    public GetAllProjects(): Observable<Project[]> {
        return this.http.get(`api/projects`, this.options)
            .map(response => response.json() as Project[])
            .catch(error => Observable.throw(error));
    }

    public GetProject(projectId: string): Observable<Project> {
        return this.http.get(`/api/projects/${projectId}`, this.options)
            .map(response => response.json() as Project)
            .catch(error => Observable.throw(error));
    }

    public SaveProject(apiKey: string, project: Project): Observable<Project> {

        return this.http.post(`/api/projects`, project, new RequestOptions({ headers: new Headers({ 'Content-Type': 'application/json', 'apiKey': apiKey }) }))
            .map(response => response.json() as Project)
            .catch(error => Observable.throw(error));
    }
}