<?php $__env->startSection('body'); ?>

    <div class="container-md">
        <div class="form-signin h-full min-vh-100 d-flex flex-column justify-content-center">

            <a class="d-flex justify-content-center mb-4 p-0 px-sm-5" href="<?php echo e(Dashboard::prefix()); ?>">
                <?php echo $__env->first([config('platform.template.header'), 'platform::header'], \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>
            </a>

            <div class="row justify-content-center">
                <div class="col-md-10 col-lg-5 col-xxl-5 px-md-5">

                    <div class="bg-white p-4 p-sm-5 rounded shadow-sm">
                        <?php echo $__env->yieldContent('content'); ?>
                    </div>
                </div>
            </div>


            <?php echo $__env->first([config('platform.template.footer'), 'platform::footer'], \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>
        </div>
    </div>

<?php $__env->stopSection(); ?>

<?php echo $__env->make('platform::app', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?><?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/auth.blade.php ENDPATH**/ ?>