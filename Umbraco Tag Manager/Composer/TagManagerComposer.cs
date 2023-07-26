using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco_Tag_Manager;

namespace Umbraco.Community.TagManager.Composer;

public class TagManagerComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Sections().Append<TagManagerSection>();
    }
}