import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';

import { ProjectService } from './services/project.service';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectsComponent } from './components/projects/projects.component';

@NgModule({
  bootstrap: [AppComponent],
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProjectComponent,
    ProjectsComponent
  ],
  imports: [
    HttpModule,
    UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'project/:id', component: ProjectComponent },
      { path: 'projects', component: ProjectsComponent },
      { path: '**', redirectTo: 'home' }
    ])
  ],
  providers: [
    ProjectService
  ]
})
export class AppModule {
}
