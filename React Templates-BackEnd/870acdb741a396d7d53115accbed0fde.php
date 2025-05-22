<?php $attributes ??= new \Illuminate\View\ComponentAttributeBag; ?>
<?php foreach($attributes->onlyProps(['target', 'action', 'push', 'rule' => \Orchid\Support\Facades\Dashboard::isPartialRequest()]) as $__key => $__value) {
    $$__key = $$__key ?? $__value;
} ?>
<?php $attributes = $attributes->exceptProps(['target', 'action', 'push', 'rule' => \Orchid\Support\Facades\Dashboard::isPartialRequest()]); ?>
<?php foreach (array_filter((['target', 'action', 'push', 'rule' => \Orchid\Support\Facades\Dashboard::isPartialRequest()]), 'is_string', ARRAY_FILTER_USE_KEY) as $__key => $__value) {
    $$__key = $$__key ?? $__value;
} ?>
<?php $__defined_vars = get_defined_vars(); ?>
<?php foreach ($attributes as $__key => $__value) {
    if (array_key_exists($__key, $__defined_vars)) unset($$__key);
} ?>
<?php unset($__defined_vars); ?>


<?php if(filter_var($rule, FILTER_VALIDATE_BOOLEAN)): ?>
    <?php $__env->startFragment($target); ?>
        <turbo-stream target="<?php echo e($target); ?>" action="<?php echo e($action ?? 'replace'); ?>">
            <template>
                <?php echo $slot; ?>

            </template>
        </turbo-stream>
    <?php echo $__env->stopFragment(); ?>
<?php elseif(!empty($push)): ?>
    <?php $__env->startPush($push); ?>
        <?php echo $slot; ?>

    <?php $__env->stopPush(); ?>
<?php else: ?>
    <?php echo $slot; ?>

<?php endif; ?>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/components/stream.blade.php ENDPATH**/ ?>