<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

use \App\Http\Controllers\TagController;
use \App\Http\Controllers\AuthController;
use \App\Http\Controllers\CategoryController;
use \App\Http\Controllers\PromocodeController;
use \App\Http\Controllers\BannerController;
use \App\Http\Controllers\SlideController;
use \App\Http\Controllers\OrderController;
use \App\Http\Controllers\ProductController;

Route::middleware('authApi')->group(function () {
  Route::get('/tags', [TagController::class, 'index']);
  Route::get('/products', [ProductController::class, 'index']);
  Route::get('/slides', [SlideController::class, 'index']);
  Route::get('/banners', [BannerController::class, 'index']);
  Route::get('/orders', [OrderController::class, 'index']);
  Route::get('/categories', [CategoryController::class, 'index']);
  Route::get('/promocodes', [PromocodeController::class, 'index']);
  Route::get('/discount', [PromocodeController::class, 'getDiscount']);

  Route::post('/auth/login', [AuthController::class, 'login']);
  Route::post('/order/create', [OrderController::class, 'create']);
  Route::post('/auth/user/update', [AuthController::class, 'updateUser']);
  Route::post('/auth/user/create', [AuthController::class, 'createNewUser']);
  Route::post('/auth/user/exists', [AuthController::class, 'ifUserExists']);
  Route::post('/auth/email/exists', [AuthController::class, 'ifEmailExists']);
  // Route::post('/auth/email/confirm', [AuthController::class, 'emailConfirm']);
  // Route::post('/auth/password/reset', [AuthController::class, 'resetPassword']);
});

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
  return $request->user();
});
