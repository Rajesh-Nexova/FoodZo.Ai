<div class="mb-3">

    <label class="form-label">
        <?php echo e(__('Email address')); ?>

    </label>

    <?php echo \Orchid\Screen\Fields\Input::make('email')
        ->type('email')
        ->required()
        ->tabindex(1)
        ->autofocus()
        ->autocomplete('email')
        ->inputmode('email')
        ->placeholder(__('Enter your email')); ?>

</div>

<div class="mb-3">
    <label class="form-label w-100">
        <?php echo e(__('Password')); ?>

    </label>

    <?php echo \Orchid\Screen\Fields\Password::make('password')
        ->required()
        ->autocomplete('current-password')
        ->tabindex(2)
        ->placeholder(__('Enter your password')); ?>

</div>

<div class="row align-items-center">
    <div class="col-md-6 col-xs-12">
        <label class="form-check">
            <input type="hidden" name="remember">
            <input type="checkbox" name="remember" value="true"
                   class="form-check-input" <?php echo e(!old('remember') || old('remember') === 'true'  ? 'checked' : ''); ?>>
            <span class="form-check-label"> <?php echo e(__('Remember Me')); ?></span>
        </label>
    </div>
    <div class="col-md-6 col-xs-12">
        <button id="button-login" type="submit" class="btn btn-default btn-block" tabindex="3">
            <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => 'bs.box-arrow-in-right','class' => 'small me-2'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
<?php $component->withName('orchid-icon'); ?>
<?php if ($component->shouldRender()): ?>
<?php $__env->startComponent($component->resolveView(), $component->data()); ?>
<?php if (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag && $constructor = (new ReflectionClass(Orchid\Icons\IconComponent::class))->getConstructor()): ?>
<?php $attributes = $attributes->except(collect($constructor->getParameters())->map->getName()->all()); ?>
<?php endif; ?>
<?php $component->withAttributes([]); ?>
<?php echo $__env->renderComponent(); ?>
<?php endif; ?>
<?php if (isset($__componentOriginal385240e1db507cd70f0facab99c4d015)): ?>
<?php $component = $__componentOriginal385240e1db507cd70f0facab99c4d015; ?>
<?php unset($__componentOriginal385240e1db507cd70f0facab99c4d015); ?>
<?php endif; ?>
            <?php echo e(__('Login')); ?>

        </button>
    </div>
</div>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/auth/signin.blade.php ENDPATH**/ ?>