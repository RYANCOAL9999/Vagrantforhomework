//
//  UnityUtil.m
//  Unity-iPhone
//
//  Created by Nicol Lee on 6/11/14.
//
//

#import "UnityUtil.h"

@implementation UnityUtil

@end
// Converts C style string to NSString
NSString* CreateNSString (const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopy (const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

extern "C" {
    const char* _getAppVersion ()
    {
        NSString *versionStr = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleShortVersionString"];
        return MakeStringCopy([versionStr UTF8String]);
    }
}

extern "C" {
    const char* _getBundleVersion ()
    {
        NSString *versionStr = [[NSBundle mainBundle] objectForInfoDictionaryKey:(NSString *)kCFBundleVersionKey];
        return MakeStringCopy([versionStr UTF8String]);
    }
}

extern "C" void _exportString(const char * eString)
{
    [UIPasteboard generalPasteboard].string = CreateNSString(eString);//@"the text to copy";
}

extern "C" bool _getFacebookInstalled()
{
    NSURL *fbURL = [NSURL URLWithString:@"fb://"];//or whatever url you're checking
    
    if ([[UIApplication sharedApplication] canOpenURL:fbURL]){
        return true;
    }
    
    return false;
}