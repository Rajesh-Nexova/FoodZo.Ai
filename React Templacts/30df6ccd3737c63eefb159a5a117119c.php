<div class="form-group">
    <?php if(isset($title)): ?>
        <label for="<?php echo e($id); ?>" class="form-label"><?php echo e($title); ?>

            <?php if(isset($attributes['required']) && $attributes['required']): ?>
                <sup class="text-danger">*</sup>
            <?php endif; ?>

            <?php if (isset($component)) { $__componentOriginal1d1976506f33d5d23fa37b3ec2628c63 = $component; } ?>
<?php $component = Orchid\Screen\Components\Popover::resolve(['content' => $popover ?? ''] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
<?php $component->withName('orchid-popover'); ?>
<?php if ($component->shouldRender()): ?>
<?php $__env->startComponent($component->resolveView(), $component->data()); ?>
<?php if (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag && $constructor = (new ReflectionClass(Orchid\Screen\Components\Popover::class))->getConstructor()): ?>
<?php $attributes = $attributes->except(collect($constructor->getParameters())->map->getName()->all()); ?>
<?php endif; ?>
<?php $component->withAttributes([]); ?>
<?php echo $__env->renderComponent(); ?>
<?php endif; ?>
<?php if (isset($__componentOriginal1d1976506f33d5d23fa37b3ec2628c63)): ?>
<?php $component = $__componentOriginal1d1976506f33d5d23fa37b3ec2628c63; ?>
<?php unset($__componentOriginal1d1976506f33d5d23fa37b3ec2628c63); ?>
<?php endif; ?>
        </label>
    <?php endif; ?>

    <?php echo e($slot); ?>


    <?php if($errors->has($oldName)): ?>
        <div class="invalid-feedback d-block">
            <small><?php echo e($errors->first($oldName)); ?></small>
        </div>
    <?php elseif(isset($help)): ?>
        <small class="form-text text-muted"><?php echo $help; ?></small>
    <?php endif; ?>
</div>

<?php if(isset($hr)): ?>
    <div class="line line-dashed border-bottom my-3"></div>
<?php endif; ?>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/partials/fields/vertical.blade.php ENDPATH**/ ?>