import { TestBed } from '@angular/core/testing';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';

import { roleGuard } from './role.guard';
import { AuthService } from '../services/auth.service';

describe('roleGuard', () => {

  const executeGuard: CanActivateFn = (...guardParameters) =>
    TestBed.runInInjectionContext(() => roleGuard(...guardParameters));

  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    authServiceSpy = jasmine.createSpyObj(
      'AuthService',
      [],
      { role: 'ADMIN' }
    );

    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });
  });

  it('should allow access when role is allowed', () => {
    Object.defineProperty(authServiceSpy, 'role', {
      get: () => 'ADMIN'
    });

    const route = {
      data: { roles: ['ADMIN'] }
    } as unknown as ActivatedRouteSnapshot;

    const result = executeGuard(route, {} as any);

    expect(result).toBeTrue();
    expect(routerSpy.navigate).not.toHaveBeenCalled();
  });

  it('should deny access when role is not allowed', () => {
    Object.defineProperty(authServiceSpy, 'role', {
      get: () => 'CLIENT'
    });

    const route = {
      data: { roles: ['ADMIN'] }
    } as unknown as ActivatedRouteSnapshot;

    const result = executeGuard(route, {} as any);

    expect(result).toBeFalse();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/auth/login']);
  });

  it('should deny access when role is null', () => {
    Object.defineProperty(authServiceSpy, 'role', {
      get: () => null
    });

    const route = {
      data: { roles: ['ADMIN'] }
    } as unknown as ActivatedRouteSnapshot;

    const result = executeGuard(route, {} as any);

    expect(result).toBeFalse();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/auth/login']);
  });

  it('should deny access when route has no roles defined', () => {
    Object.defineProperty(authServiceSpy, 'role', {
      get: () => 'ADMIN'
    });

    const route = {
      data: {}
    } as ActivatedRouteSnapshot;

    const result = executeGuard(route, {} as any);

    expect(result).toBeFalse();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/auth/login']);
  });
});
