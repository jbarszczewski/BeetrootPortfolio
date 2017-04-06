import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { RecaptchaModule } from 'ng-recaptcha';
import { RecaptchaFormsModule } from 'ng-recaptcha/forms';
import { UniversalModule } from 'angular2-universal';

import { ProjectService } from './services/project.service';

import { AppComponent } from './components/app/app.component';
import { EditorComponent } from './components/editor/editor.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectsComponent } from './components/projects/projects.component';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        EditorComponent,
        HomeComponent,
        NavMenuComponent,
        ProjectComponent,
        ProjectsComponent
    ],
    imports: [
        FormsModule,
        HttpModule,
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RecaptchaModule.forRoot(),
        RecaptchaFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'editor', component: EditorComponent },
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
