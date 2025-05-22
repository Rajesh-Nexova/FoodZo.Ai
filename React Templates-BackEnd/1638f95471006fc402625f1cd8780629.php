<?php $__env->startSection('body'); ?>

    <div class="container-xl p-0 h-100">
        <div class="workspace workspace-limit pt-3 pt-md-4 mb-4 mb-md-0 d-flex flex-column h-100">
            <?php echo $__env->yieldContent('workspace'); ?>

            <?php echo $__env->first([config('platform.template.footer'), 'platform::footer'], \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>
        </div>
    </div>

<?php $__env->stopSection(); ?>

<?php echo $__env->make('platform::app', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?><?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/workspace/compact.blade.php ENDPATH**/ ?>