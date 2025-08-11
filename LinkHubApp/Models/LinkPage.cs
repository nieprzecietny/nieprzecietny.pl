using System.Collections.Generic;

namespace LinkHubApp.Models;

public record SocialLink(string Icon, string Url, string Text);
public record LinkItem(string Icon, string Url, string Text);
public record LinkPage(
    string BannerUrl,
    string AvatarUrl,
    string Title,
    List<SocialLink> SocialLinks,
    List<LinkItem> Links,
    string FooterText);
