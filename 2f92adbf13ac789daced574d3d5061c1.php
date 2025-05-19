<?php if(!\Orchid\Platform\Dashboard::assetsAreCurrent()): ?>
    <div class="alert alert-warning rounded shadow-sm mb-3 p-4" data-turbo-temporary>
        <div class="d-flex align-items-center mb-2 text-black">
            <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => 'bs.exclamation-triangle','width' => '2em','height' => '2em','class' => 'me-2'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
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
            <h4 class="mb-0 fw-light"><?php echo e(__("Complete the update.")); ?></h4>
        </div>

        <p><?php echo e(__("The published Orchid assets are not up-to-date with the installed version. To update, run:")); ?></p>
        <code class="user-select-all">php artisan orchid:publish</code>
    </div>
<?php endif; ?>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/partials/update-assets.blade.php ENDPATH**/ ?>