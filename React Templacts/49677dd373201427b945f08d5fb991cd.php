<?php $__env->startSection('title', e(__($name))); ?>
<?php $__env->startSection('description', e(__($description))); ?>
<?php $__env->startSection('controller', 'base'); ?>

<?php $__env->startSection('navbar'); ?>
    <?php $__currentLoopData = $commandBar; $__env->addLoop($__currentLoopData); foreach($__currentLoopData as $command): $__env->incrementLoopIndices(); $loop = $__env->getLastLoop(); ?>
        <li class="<?php echo e(!$loop->first ? 'ms-2' : ''); ?>">
            <?php echo $command; ?>

        </li>
    <?php endforeach; $__env->popLoop(); $loop = $__env->getLastLoop(); ?>
<?php $__env->stopSection(); ?>

<?php $__env->startSection('content'); ?>
    <div id="modals-container">
        <?php echo $__env->yieldPushContent('modals-container'); ?>
    </div>

    <form id="post-form"
          class="mb-md-4 h-100"
          method="post"
          enctype="multipart/form-data"
          data-controller="form"
          data-form-need-prevents-form-abandonment-value="<?php echo e(var_export($needPreventsAbandonment)); ?>"
          data-form-failed-validation-message-value="<?php echo e($formValidateMessage); ?>"
          data-action="keypress->form#disableKey
                      turbo:before-fetch-request@document->form#confirmCancel
                      beforeunload@window->form#confirmCancel
                      change->form#changed
                      form#submit"
          novalidate
    >
        <?php echo $layouts; ?>

        <?php echo csrf_field(); ?>
        <?php echo $__env->make('platform::partials.confirm', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>
    </form>

    <div data-controller="filter">
        <form id="filters" autocomplete="off"
              data-action="filter#submit"
              data-form-need-prevents-form-abandonment-value="false"
        ></form>
    </div>

    <?php echo $__env->renderWhen(isset($state), 'platform::partials.state', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path'])); ?>
<?php $__env->stopSection(); ?>

<?php echo $__env->make('platform::dashboard', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?><?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/layouts/base.blade.php ENDPATH**/ ?>