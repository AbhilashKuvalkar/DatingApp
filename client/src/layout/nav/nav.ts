import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast-service';
import { themes } from '../theme';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  private readonly toastService = inject(ToastService);
  private readonly router = inject(Router);

  protected accountService = inject(AccountService);
  protected creds: any = {};
  protected selectedTheme = signal<string>(localStorage.getItem("theme") || "default");
  protected themes = themes;

  ngOnInit(): void {
    document.documentElement.setAttribute("data-theme", this.selectedTheme());
  }

  handleSelectTheme(theme: string) {
    this.selectedTheme.set(theme);
    localStorage.setItem("theme", theme);
    document.documentElement.setAttribute("data-theme", theme);
  }

  login() {
    this.accountService
      .login(this.creds)
      .subscribe({
        next: result => {
          this.router.navigateByUrl("/members");
          this.toastService.success("Logged in successfully");
          this.creds = {};
        },
        error: error => {
          console.error(error);
          if (typeof error.error === "string")
            this.toastService.error(error.error);
          else {
            let message = "";

            if (error.error.errors) {

              Object.keys(error.error.errors).forEach(key => {
                const arr = error.error.errors[key];

                if (arr && Array.isArray(arr)) {
                  message += arr.join(" ");
                  message += "<br />";
                }

              });

            }

            if (message.trim() !== "") {
              this.toastService.error(message);
            }
          }
        },
        complete: () => console.log("HTTP service completed")
      });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl("/");
  }
}
