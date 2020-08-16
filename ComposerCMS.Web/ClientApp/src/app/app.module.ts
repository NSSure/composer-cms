import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { MainComponent } from './main.component';
import { LoginComponent } from './login/login.component';
import { SiteSettingsComponent } from './settings/site-settings.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ResourcesComponent } from './resources/resources.component';
import { FileUploadComponent } from './file/upload/file-upload.component';
import { ResourceListComponent } from './resources/resource-list.component';
import { HtmlEditorComponent } from './html-editor/html-editor.component';
import { ActivityHistoryComponent } from './activity-history/activity-history.component';
import { DiffComponent } from './diff/diff.component';
import { PageListComponent } from './structure/page/page-list.component';
import { PageManageComponent } from './structure/page/page-manage.component';
import { LayoutListComponent } from './structure/layout-list.component';
import { LayoutManageComponent } from './structure/layout-manage.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { PageVersionListComponent } from './structure/page/version/page-version-list.component';
import { MenuListComponent } from './structure/menu/menu-list.component';
import { MenuManageComponent } from './structure/menu/menu-manage-component';
import { BlogListComponent } from './blog/blog-list.component';
import { BlogManageComponent } from './blog/blog-manage.component';
import { PostManageComponent } from './post/post-manage.component';
import { PostListComponent } from './post/post-list.component';
import { ActiveStateIndicatorComponent } from './helpers/active-state-indicator.component';
import { MenuItemListComponent } from './structure/menu/menu-item-list.component';
import { AccountService } from './services/account.service';
import { AuthenticatedGuard } from './guards/authenticated.guard';
import { AuthInterceptor } from './interceptors/auth.intercecptor';
import { ThemeListComponent } from './structure/theme/theme-list.component';

import { CodemirrorModule } from '@ctrl/ngx-codemirror';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', component: MainComponent, canActivate: [AuthenticatedGuard], children: [
      { path: '', component: DashboardComponent, data: { pageTitle: 'Dashboard' } },
      { path: 'layouts', component: LayoutListComponent, data: { pageTitle: 'Layouts' } },
      { path: 'layout', component: LayoutManageComponent, data: { pageTitle: 'Layout' } },
      { path: 'pages', component: PageListComponent, data: { pageTitle: 'Pages' } },
      { path: 'page', component: PageManageComponent, data: { pageTitle: 'Create New Page' } },
      { path: 'page/:id', component: PageManageComponent, data: { pageTitle: 'Manage Page' } },

      { path: 'menus', component: MenuListComponent, data: { pageTitle: 'Menus' } },
      { path: 'menu', component: MenuManageComponent, data: { pageTitle: 'Create New Menu' } },
      { path: 'menu/:menuId', component: MenuManageComponent, data: { pageTitle: 'Manage Menu' } },
      
      { path: 'blogs', component: BlogListComponent, data: { pageTitle: 'Blogs' } },
      { path: 'blog', component: BlogManageComponent, data: { pageTitle: 'Create New Blog' } },
      { path: 'blog/:blogId', component: BlogManageComponent, data: { pageTitle: 'Manage Blog' } },
      { path: 'blog/:blogId/post', component: PostManageComponent, data: { pageTitle: 'Create New Post' } },
      { path: 'blog/:blogId/post/:postId', component: PostManageComponent, data: { pageTitle: 'Manage Post' } },

      { path: 'resources', component: ResourcesComponent, data: { pageTitle: 'Resources' } },
      { path: 'themes', component: ThemeListComponent, data: { pageTitle: 'Themes' } },
      { path: 'activity', component: ActivityHistoryComponent, data: { pageTitle: 'Activity History' } },
      { path: 'settings', component: SiteSettingsComponent, data: { pageTitle: 'Settings' } },
      { path: 'diff/:pageId/:pageVersionId', component: DiffComponent, data: { pageTitle: 'Diff Comparison' } },
    ]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    LoginComponent,
    SideMenuComponent,
    PageListComponent,
    PageManageComponent,
    PageVersionListComponent,
    ActivityHistoryComponent,
    ThemeListComponent,
    SiteSettingsComponent,
    DashboardComponent,
    LayoutListComponent,
    LayoutManageComponent,
    MenuListComponent,
    MenuManageComponent,
    MenuItemListComponent,
    ResourcesComponent,
    ResourceListComponent,
    FileUploadComponent,
    HtmlEditorComponent,
    DiffComponent,
    BlogListComponent,
    BlogManageComponent,
    PostListComponent,
    PostManageComponent,
    ActiveStateIndicatorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    CodemirrorModule
  ],
  providers: [
    AccountService,
    AuthenticatedGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthInterceptor
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
