using System;
using Our.Umbraco.TagManager.Migrations;
using Umbraco.Cms.Core.Packaging;

namespace MediaWiz.Forums.Migrations
{
    public class MediaWizPackageMigrationPlan : PackageMigrationPlan
    {
        public MediaWizPackageMigrationPlan()
            : base("Umbraco TagManager")
        {
        }

        protected override void DefinePlan()
        {

            From(String.Empty)
                .To<TagManagerMigrationHelper>("UsomeTagManagerMigrationv1-db");;

        }
    }
}
