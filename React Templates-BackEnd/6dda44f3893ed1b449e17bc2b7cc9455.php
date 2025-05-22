<?php $__env->startSection('aside'); ?>
    <div class="aside col-xs-12 col-xxl-2 bg-dark d-flex flex-column" data-controller="menu">
        <header class="d-xl-block p-3 mt-xl-4 w-100 d-flex align-items-center">
            <a href="#" class="header-toggler d-xl-none me-auto order-first d-flex align-items-center lh-1"
               data-action="click->menu#toggle">
                <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => 'bs.three-dots-vertical','class' => 'icon-menu'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
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

                <span class="ms-2"><?php echo $__env->yieldContent('title'); ?></span>
            </a>

            <a class="header-brand order-last" href="<?php echo e(route(config('platform.index'))); ?>">
                <?php echo $__env->first([config('platform.template.header'), 'platform::header'], \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>
            </a>
        </header>

        <nav class="aside-collapse w-100 d-xl-flex flex-column collapse-horizontal" id="headerMenuCollapse">

            <?php echo $__env->make('platform::partials.search', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>

            <ul class="nav flex-column mb-md-1 mb-auto ps-0">
                <?php echo Dashboard::renderMenu(\Orchid\Platform\Dashboard::MENU_MAIN); ?>

            </ul>

            <div class="h-100 w-100 position-relative to-top cursor d-none d-md-flex mt-md-5"
                 data-action="click->html-load#goToTop"
                 title="<?php echo e(__('Scroll to top')); ?>">
                <div class="bottom-left w-100 mb-2 ps-3 overflow-hidden">
                    <small data-controller="viewport-entrance-toggle"
                           class="scroll-to-top"
                           data-viewport-entrance-toggle-class="show">
                        <?php if (isset($component)) { $__componentOriginal385240e1db507cd70f0facab99c4d015 = $component; } ?>
<?php $component = Orchid\Icons\IconComponent::resolve(['path' => 'bs.chevron-up','class' => 'me-2'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
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
                        <?php echo e(__('Scroll to top')); ?>

                    </small>
                </div>
            </div>

            <footer class="position-sticky bottom-0">
                <div class="bg-dark position-relative overflow-hidden" style="padding-bottom: 10px;">
                    <?php echo $__env->renderWhen(Auth::check(), 'platform::partials.profile', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path'])); ?>
                </div>


                

            </footer>
        </nav>
    </div>
<?php $__env->stopSection(); ?>

<?php $__env->startSection('workspace'); ?>
    <?php if(Breadcrumbs::has()): ?>
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb px-4 mb-2">
                <?php if (isset($component)) { $__componentOriginalc8c240fa0835e018de1219d3a62bdff3 = $component; } ?>
<?php $component = Tabuna\Breadcrumbs\BreadcrumbsComponent::resolve(['class' => 'breadcrumb-item','active' => 'active'] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
<?php $component->withName('tabuna-breadcrumbs'); ?>
<?php if ($component->shouldRender()): ?>
<?php $__env->startComponent($component->resolveView(), $component->data()); ?>
<?php if (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag && $constructor = (new ReflectionClass(Tabuna\Breadcrumbs\BreadcrumbsComponent::class))->getConstructor()): ?>
<?php $attributes = $attributes->except(collect($constructor->getParameters())->map->getName()->all()); ?>
<?php endif; ?>
<?php $component->withAttributes([]); ?>
<?php echo $__env->renderComponent(); ?>
<?php endif; ?>
<?php if (isset($__componentOriginalc8c240fa0835e018de1219d3a62bdff3)): ?>
<?php $component = $__componentOriginalc8c240fa0835e018de1219d3a62bdff3; ?>
<?php unset($__componentOriginalc8c240fa0835e018de1219d3a62bdff3); ?>
<?php endif; ?>
            </ol>
        </nav>
    <?php endif; ?>

    <div class="order-last order-md-0 command-bar-wrapper">
        <div class="<?php if (! empty(trim($__env->yieldContent('navbar')))): ?> <?php else: ?> d-none d-md-block <?php endif; ?> layout d-md-flex align-items-center">
            <header class="d-none d-md-block col-xs-12 col-md p-0 me-3">
                <h1 class="m-0 fw-light h3 text-black"><?php echo $__env->yieldContent('title'); ?></h1>
                <small class="text-muted" title="<?php echo $__env->yieldContent('description'); ?>"><?php echo $__env->yieldContent('description'); ?></small>
            </header>
            <nav class="col-xs-12 col-md-auto ms-md-auto p-0">
                <ul class="nav command-bar justify-content-sm-end justify-content-start d-flex align-items-center">
                    <?php echo $__env->yieldContent('navbar'); ?>
                </ul>
            </nav>
        </div>
    </div>

    <?php echo $__env->make('platform::partials.alert', \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?>
    <?php echo $__env->yieldContent('content'); ?>
<?php $__env->stopSection(); ?>

<?php echo $__env->make(config('platform.workspace', 'platform::workspace.compact'), \Illuminate\Support\Arr::except(get_defined_vars(), ['__data', '__path']))->render(); ?><?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/dashboard.blade.php ENDPATH**/ ?>