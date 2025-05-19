<?php $__env->startPush('head'); ?>
    <meta name="robots" content="noindex"/>
    <meta name="google" content="notranslate">
    <link
          href="<?php echo e(asset('/vendor/orchid/favicon.svg')); ?>"
          sizes="any"
          type="image/svg+xml"
          id="favicon"
          rel="icon"
    >

    <!-- For Safari on iOS -->
    <meta name="theme-color" content="#21252a">
<?php $__env->stopPush(); ?>

<div class="h2 d-flex align-items-center">
    <?php if(auth()->guard()->check()): ?>
        <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => 'bs.house','class' => 'd-inline d-xl-none'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
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
    <?php endif; ?>


    <p class="my-0 <?php echo e(auth()->check() ? 'd-none d-xl-block' : ''); ?>">
        <?php echo e(config('app.name')); ?>

        <small class="align-top opacity">Orchid</small>
    </p>
</div>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/header.blade.php ENDPATH**/ ?>