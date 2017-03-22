import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Project } from '../models/project';

@Injectable() export class ProjectService {
  headers: Headers;
  options: RequestOptions;

  constructor(private http: Http) {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
  }

  public GetProject(projectId: number): Observable<Project> {
    return this.http.get(`/api/projects/${projectId}`)
      .map((res: Response) => {
        return res.json() as Project;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      })
  }
}