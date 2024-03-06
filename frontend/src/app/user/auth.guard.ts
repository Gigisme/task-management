import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {CurrentUserService} from "./current-user.service";
import {filter, map} from "rxjs";

export const authGuard: CanActivateFn = (route, state) => {
    const router = inject(Router)
    const userService = inject(CurrentUserService)

    return userService.currentUser$.pipe(
        filter((currentUser) => currentUser !== undefined),
        map((currentUser) => {
            if (!currentUser) {
                router.navigate(['/login']);
                return false;
            }
            return true;
        })
    )
};
