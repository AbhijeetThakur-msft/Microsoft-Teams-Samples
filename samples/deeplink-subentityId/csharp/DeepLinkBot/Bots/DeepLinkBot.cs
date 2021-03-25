﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class DeepLinkBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var attachment = AdaptiveDeepLinkCard(turnContext.Activity.From.Name);

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        public static Attachment AdaptiveDeepLinkCard(string userName)
        {
            var DeepLinkCard = new AdaptiveCard(new AdaptiveSchemaVersion("1.0"))
            {
                Body = new List<AdaptiveElement>()
                {
                    new AdaptiveContainer
                    {
                        Items=new List<AdaptiveElement>()
                        {
                            new AdaptiveTextBlock()
                            {
                                Text=$"Hey {userName}! Please click on below buttons to navigate to a tab!",
                                Size=AdaptiveTextSize.Large,
                                Wrap=true
                            },          
                           
                            new AdaptiveColumnSet()
                            {
                                Columns=new List<AdaptiveColumn>()
                                {                                  
                                   new AdaptiveColumn()
                                    {
                                         Width=AdaptiveColumnWidth.Auto,
                                         Items=new List<AdaptiveElement>()
                                         {
                                             new AdaptiveTextBlock(){Text="Topic1",Color=AdaptiveTextColor.Accent,Size=AdaptiveTextSize.Medium,HorizontalAlignment=AdaptiveHorizontalAlignment.Center,Spacing=AdaptiveSpacing.None}
                                         },
                                           SelectAction = new AdaptiveOpenUrlAction()
                                         {
                                             Url=new Uri(DeeplinkHelper.Task1Deeplink),
                                             Title = "Topic1"
                                         }
                                    }
                                }
                            },
                             new AdaptiveColumnSet()
                            {
                                Columns=new List<AdaptiveColumn>()
                                {
                                   new AdaptiveColumn()
                                    {
                                         Width=AdaptiveColumnWidth.Auto,
                                         Items=new List<AdaptiveElement>()
                                         {
                                             new AdaptiveTextBlock(){Text="Topic2",Color=AdaptiveTextColor.Accent,Size=AdaptiveTextSize.Medium,HorizontalAlignment=AdaptiveHorizontalAlignment.Center,Spacing=AdaptiveSpacing.None}
                                         },
                                           SelectAction = new AdaptiveOpenUrlAction()
                                         {
                                             Url=new Uri(DeeplinkHelper.Task2Deeplink),
                                             Title = "Topic2"
                                         }
                                    }
                                }
                            },
                               new AdaptiveColumnSet()
                            {
                                Columns=new List<AdaptiveColumn>()
                                {
                                   new AdaptiveColumn()
                                    {
                                         Width=AdaptiveColumnWidth.Auto,
                                         Items=new List<AdaptiveElement>()
                                         {
                                             new AdaptiveTextBlock(){Text="Topic3",Color=AdaptiveTextColor.Accent,Size=AdaptiveTextSize.Medium,HorizontalAlignment=AdaptiveHorizontalAlignment.Center,Spacing=AdaptiveSpacing.None}
                                         },
                                           SelectAction = new AdaptiveOpenUrlAction()
                                         {
                                             Url=new Uri(DeeplinkHelper.Task3Deeplink),
                                             Title = "Topic3"
                                         }
                                    }
                                }
                            }
                        }
                    }
             }
            };

            var acard = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = DeepLinkCard
            };
            return acard;
        }
    }
}