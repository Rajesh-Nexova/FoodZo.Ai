<?php $__env->startComponent($typeForm, get_defined_vars()); ?>
    <button type="button"
            <?php echo e($attributes); ?>

            data-controller="modal-toggle"
            data-action="click->modal-toggle#targetModal"
            data-modal-toggle-title="<?php echo e($modalTitle ?? $title ??  ''); ?>"
            data-modal-toggle-key="<?php echo e($modal ?? ''); ?>"
            data-modal-toggle-async="<?php echo e($async); ?>"
            data-modal-toggle-params='<?php echo json_encode($parameters, 15, 512) ?>'
            data-modal-toggle-action="<?php echo e($action); ?>"
            data-modal-toggle-open="<?php echo e($open); ?>"
    >

        <?php if(isset($icon)): ?>
            <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => $icon,'class' => ''.e(empty($name) ?: 'me-2').''] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
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

        <span><?php echo e($name ?? ''); ?></span>
    </button>
<?php echo $__env->renderComponent(); ?>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/actions/modal.blade.php ENDPATH**/ ?>