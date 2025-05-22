<?php $__env->startComponent($typeForm, get_defined_vars()); ?>
    <div data-controller="datetime"
         class="input-group"
        <?php echo e($dataAttributes); ?>>
        <input type="text"
               placeholder="<?php echo e($placeholder ?? ''); ?>"
               <?php echo e($attributes); ?>

               autocomplete="off"
               data-datetime-target="instance"
        >

        <?php if(true === $allowEmpty): ?>
            <div class="input-group-append bg-white">
                <a class="input-group-text h-100 text-muted"
                   title="clear"
                   data-action="click->datetime#clear">
                        <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => 'bs.x-lg','class' => 'm-0 p-0'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
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
                    </a>
                </div>
            <?php endif; ?>
        </div>
<?php echo $__env->renderComponent(); ?>




<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/fields/datetime.blade.php ENDPATH**/ ?>